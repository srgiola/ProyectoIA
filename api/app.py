from flask import Flask
from data.user import User
from helps.database import DataBaseUtil

app = Flask(__name__)

@app.route('/')
def home():
    return "API en corriendo 👍"

@app.route('/validate')
def validate ():
    print('start')
    connection = DataBaseUtil.get_connection()

if __name__ == '__main__':
    app.run(debug=True, host='0.0.0.0', port=5000)