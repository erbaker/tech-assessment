from flask import Flask
from flask_restx import Resource, Api
from api.config import DevelopmentConfig

# Create an instance of the Flask app
app = Flask(__name__)
app.config.from_object(DevelopmentConfig)

api = Api(app)

orders = []

# Create order endpoint
@api.route('/orders', methods=['POST'])
class CreateOrder(Resource):
    def post(self):
        order = api.payload
        orders.append(order)
        return {'message': 'Order created successfully'}, 201
    
# Orders by customer endpoint
@api.route('/orders/<customer_name>')
class OrdersByCustomer(Resource):
    def get(self, customer_name):
        customer_orders = [order for order in orders if order['customerName'] == customer_name]
        return {'orders': customer_orders}
    
# Update order endpoint

# Cancel order endpoint


class Health(Resource):
    def get(self):
        return {
            'status': 'success',
            'message': 'You keep using that word. I do not think it means what you think it means.'
        }

api.add_resource(Health, '/health')