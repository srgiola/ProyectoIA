import pandas as pd
import joblib
import nltk
from collections import defaultdict
from sklearn.model_selection import train_test_split
from math import log
import re
import string

nltk.download('punkt')

# Ruta del dataset
dataset_path = 'dataset.csv'

# Inicializar los diccionarios para las Bag of Words
frecuencies_fresh = defaultdict(int)
frecuencies_rotten = defaultdict(int)

# Contadores para las probabilidades 
count_fresh = 0
count_rotten = 0

# Tamaño del chunk
chunk_size = 100000

# Dividir los datos en entrenamiento y validación
train_data_chunks = []
valid_data_chunks = []

for chunk in pd.read_csv(dataset_path, chunksize=chunk_size):
    train_chunk, valid_chunk = train_test_split(chunk, test_size=0.2, random_state=42)
    train_data_chunks.append(train_chunk)
    valid_data_chunks.append(valid_chunk)

# Guardar los datos de validación y entrenamiento en un archivo pickle
valid_data = pd.concat(valid_data_chunks)
valid_data.to_pickle('pkl/valid_data.pkl')
train_data = pd.concat(train_data_chunks)
train_data.to_pickle('pkl/train_data.pkl')

def clean_text(text):
    # Convertir todo el texto a minúsculas
    text = text.lower()
    
    # Eliminar signos de puntuación especificados
    text = re.sub(r'[.,;[\](){}!¡¿?/\~+\-@<>"\'#%&=|]', '', text)
    
    # Sustituir los signos "-" y "_" por un espacio
    text = text.replace('-', ' ').replace('_', ' ')
    
    # Eliminar saltos de línea y tabulaciones
    text = text.replace('\n', ' ').replace('\t', ' ')
    
    # Eliminar dobles espacios
    text = re.sub(' +', ' ', text)
    
    # Eliminar espacios al inicio y al final
    text = text.strip()
    
    return text

# Leer el archivo CSV en chunks
for chunk in train_data_chunks:
    # Procesar cada fila del chunk
    for record, review_type in zip(chunk['review_content'], chunk['review_type']):
        # Verificar si el valor es NaN
        if pd.isna(record) or pd.isna(review_type):
            continue
        # Limpiar el texto antes de tokenizar
        cleaned_record = clean_text(record)
        tokens = nltk.word_tokenize(cleaned_record, language='english')
        # Determinar a que BoW pertenecen los tokens
        if review_type == 'fresh':
            count_fresh += 1
            for token in tokens:
                frecuencies_fresh[token] += 1
        elif review_type == 'rotten':
            count_rotten += 1
            for token in tokens:
                frecuencies_rotten[token] += 1

# Probabilidades de "fresh" y "rotten" en terminos logaritmicos (Probabilidades previas)
total_reviews = count_fresh + count_rotten
log_prior_fresh = log(count_fresh / total_reviews)
log_prior_rotten = log(count_rotten / total_reviews)

# Calcular el total de palabras en cada categoría
total_fresh = sum(frecuencies_fresh.values()) # frecuencia de "fresh"
total_rotten = sum(frecuencies_rotten.values()) # frecuencia de "rotten"

# Funcíon para calcular la tabla de probabilidades condicionales (CPT)
def Get_CPT(frecuency_table, total):
    tmp_cpt = {}
    for word, frecuency in frecuency_table.items():
        probability = frecuency / total
        tmp_cpt[word] = probability
    return tmp_cpt

# CPTs
conditional_prob_table_fresh = Get_CPT(frecuencies_fresh, total_fresh)
conditional_prob_table_rotten = Get_CPT(frecuencies_rotten, total_rotten)

# Guardar el modelo entrenado
model = {
    'log_prior_fresh': log_prior_fresh,
    'log_prior_rotten': log_prior_rotten,
    'conditional_prob_table_fresh': conditional_prob_table_fresh,
    'conditional_prob_table_rotten': conditional_prob_table_rotten,
    'total_fresh': total_fresh,
    'total_rotten': total_rotten
}
joblib.dump(model, 'pkl/model.pkl')

print(f"count_fresh '{count_fresh}'")
print(f"count_rotten '{count_rotten}'")
print("Modelo Entrenado")