//customer model for populating customer testing data
const mongoose = require('mongoose')

const customerSchema = mongoose.Schema({
    firstName:{
        type:String, 
        required: true
    }, 
    lastName:{
        type:String, 
        required: true
    }, 
    age:{
        type:String, 
        required: true
    }, 
    orders: [{
       type: mongoose.Schema.Types.ObjectId,
       ref : "Order"
    }], 
    address : {
        type: mongoose.Schema.Types.ObjectId,
        ref : "Address"
    }, 
   phoneNumber: {
       type:String, 
       required: true
   },
   join_date:{
       type:Date,
       default:Date.now
   }
})
module.exports = mongoose.model('Customer', customerSchema)