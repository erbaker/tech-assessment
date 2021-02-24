const express = require('express')
const router = express.Router()
const orderModel = require('../models/Order')
const customerModel = require('../models/Customer')
//Home
router.get('/', (req, res) => {
    res.json({
        body: {
       message: "Orders API Home"
     }
 })
 })
//create order endpoint
router.post('/customers/:id/orders/create', async(req, res) => {
    const customer_id = req.params.id
    const customer = await customerModel.findById(customer_id)
    const orders = new orderModel({
        customer : customer_id,
        description : req.body.description
    })
    const order_id = orders._id
    customer.orders.push(order_id)
    customer.save()
    orders.save()   
    .then(resp=>{
        res.send(resp)
    })
    .catch(err=>{
        res.send(err)
    })
 })

 module.exports = router