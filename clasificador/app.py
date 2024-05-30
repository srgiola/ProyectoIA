from flask import Flask, request, jsonify
from clasificador import Is_Fresh_Or_Rotten

app = Flask(__name__)

@app.route('/')
def home():
    return "API en corriendo 👍"

@app.route('/classify', methods=['POST'])
def classify():
    data = request.get_json()
    critic = data.get('critic')
    if not critic:
        return jsonify({"error": "Missing 'critic' field in JSON data"}), 400

    result = Is_Fresh_Or_Rotten(critic)
    return jsonify({"result": result})

if __name__ == '__main__':
    app.run(debug=True, host='0.0.0.0', port=5000)