import pandas as pd
from clasificador import Is_Fresh_Or_Rotten

# Cargar los datos de validación y entrenamiento desde los archivos pickle
valid_data = pd.read_pickle('pkl/valid_data.pkl')
train_data = pd.read_pickle('pkl/train_data.pkl')

# Evaluar el modelo en el conjunto de datos
def evaluate_model(data, label):
    correct = 0
    total = 0
    for record, review_type in zip(data['review_content'], data['review_type']):
        # Verificar si el valor es NaN o está vacío
        if pd.isna(record) or pd.isna(review_type) or record == '' or review_type == '':
            continue
        prediction = Is_Fresh_Or_Rotten(record)
        if prediction == review_type:
            correct += 1
        total += 1
    accuracy = correct / total
    return accuracy

print("Evaluando...")

train_accuracy = evaluate_model(train_data, "Training")
print(f"Presición del modelo: {train_accuracy:.2f}")
valid_accuracy = evaluate_model(valid_data, "Validation")
print(f"Presición de la evaluación: {valid_accuracy:.2f}")

# Verificar si el modelo está sobreentrenado
if train_accuracy > valid_accuracy + 0.1:
    print("El modelo podría estar sobreentrenado.")
else:
    print("El modelo no está sobreentrenado.")