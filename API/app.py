from flask import Flask, jsonify, request, redirect

app = Flask(__name__)

# This is our api. It will run on all interface from now 
# but can also run on localhost. His role will be to feed data 
# to the app and verify connections as if it had a database

@app.route("/")
def main():
    """
    Docstring pour main
    """
    return redirect("/api", 302)

@app.route("/api", methods=['GET', 'POST'])
def api_index():
    """
    Docstring for api_index
    """
    # Test the post method. When this route is call,
    # teh API try to get the data sent by json and if
    # all is correct with them send a message and a 200 code
    # will polish next time.
     
    if request.method == 'POST':
        data = request.get_json(silent=True)
        print(data)
        if data :
            return jsonify("Donnée reçu"), 200
        else:
            return jsonify("Données pas reçu"), 400

    liste = ['banane', 'orange', 'pomme']
    return jsonify(liste)

@app.route("/api/connexion", methods=['GET','POST'])
def api_connexion():
    """Route whose role will be to verify user information"""
    if request.method == 'POST':
        return "Post method done"
    
    return 'Get method done'

if __name__ == '__main__':
    app.run('0.0.0.0')
