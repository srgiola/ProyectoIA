import nltk
import joblib
from math import log

nltk.download('punkt')

# Cargar el modelo entrenado
model = joblib.load('pkl/model.pkl')

log_prior_fresh = model['log_prior_fresh']
log_prior_rotten = model['log_prior_rotten']
conditional_prob_table_fresh = model['conditional_prob_table_fresh']
conditional_prob_table_rotten = model['conditional_prob_table_rotten']
total_fresh = model['total_fresh']
total_rotten = model['total_rotten']

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

# Ejemplo de uso (puedes comentar o eliminar esta parte si no es necesario)
if __name__ == "__main__":
    message = "to the fantasy"
    review_type = Is_Fresh_Or_Rotten(message)
    print(f"El mensaje '{message}' es clasificado como: {review_type}")