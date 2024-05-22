import pandas as pd
import nltk
from collections import defaultdict
from math import log

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

# Leer el archivo CSV en chunks
for chunk in pd.read_csv(dataset_path, chunksize=chunk_size):
    # Procesar cada fila del chunk
    for record, review_type in zip(chunk['review_content'], chunk['review_type']):
        tokens = nltk.word_tokenize(record, language='english')
        # Determinar a que BoW pertenen los tokens
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

# Inferencia de Naive Bayes
def Get_Message_Probability(message, log_prior, conditional_prob_table, total):
    tokens = nltk.word_tokenize(message, language='english')
    
    # Se utilizara un "Suavizado de Laplace" cuando el token no está en la cpt, para evitar underflow numerico
    laplace_smoothing = log(1 / (total + len(conditional_prob_table)))

    for token in tokens:
        if token in conditional_prob_table:
            log_prior += log(conditional_prob_table[token])
        else:
            log_prior += laplace_smoothing
    
    return log_prior

# Función para predecir si un mensaje es "fresh" o "rotten"
def Is_Fresh_Or_Rotten(message):
    log_prob_fresh = Get_Message_Probability(message, log_prior_fresh, conditional_prob_table_fresh, total_fresh)
    log_prob_rotten = Get_Message_Probability(message, log_prior_rotten, conditional_prob_table_rotten, total_rotten)
    
    if log_prob_fresh > log_prob_rotten:
        return "fresh"
    else:
        return "rotten"

# Ejemplo de uso
message = "to the fantasy"
review_type = Is_Fresh_Or_Rotten(message)
print(f"El mensaje '{message}' es clasificado como: {review_type}")