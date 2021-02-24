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

 //list all orders by customer endpoint
 router.post('/customers/:id/orders', async(req, res) => {
    const customer_id = req.params.id
    const orders = await orderModel.find({}).find({ customer: { $gte: customer_id} })
    try{
        res.send(orders)
    }catch(err){
        res.send(err)
    }
})
 //update order endpoint
 router.patch('/customers/:user_id/orders/:order_id', async(req, res) => {
    const order_id = req.params.order_id
    const order = await orderModel.findById(order_id)
    order.description = req.body.description
    order.save()
    try{
        res.send(order)
    }catch(err){
        res.send(err)
    }
})
 //cancel order endpoint
router.delete('/orders/:order_id', async(req, res) => {
  const order_id = req.params.order_id
  const delete_order = await orderModel.remove({
      _id : order_id
  })
  try{
    res.send(delete_order)
   }catch(err){
    res.send(err)
  }
})
 module.exports = router