//orders model for populating orders data
const mongoose = require('mongoose')

const orderSchema = mongoose.Schema({
     customer: {
        type: mongoose.Schema.Types.ObjectId,
        ref : "Customer"
     }, 
     description: {
         type: String,
         required : true
     },
     order_date:{
         type:Date,
         default:Date.now
     }
})
module.exports = mongoose.model('Order', orderSchema)