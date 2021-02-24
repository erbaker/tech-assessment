const express = require('express')
const router = express.Router()
const customerModel = require('../models/Customer')
const addressModel = require('../models/Address')
//Home
router.get('/', (req, res) => {
    res.json({
        body: {
       message: "Orders API Home"
     }
 })
 })
//added creating customers request for testing purposes
 router.post('/customers/create', (req, res) => {
    const address = new addressModel(req.body.address)
    const address_id = address._id
    const customer = new customerModel({
        firstName : req.body.firstName, 
        lastName : req.body.lastName, 
        age : req.body.age, 
        address : address_id, 
        phoneNumber : req.body.phoneNumber
    })
    address.customer = customer._id; 
    address.save()
    customer.save() 
    .then(resp=>{
        res.send(resp)
    })
    .catch(err=>{
        res.send(err)
    })

 })
 module.exports = router
