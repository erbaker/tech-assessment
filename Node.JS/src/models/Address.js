//Address model for populating customer testing
const mongoose = require('mongoose')

const addressSchema = mongoose.Schema({
    streetAddress:{
        type:String, 
        required: true
    }, 
    city:{
        type:String, 
        required: true
    }, 
    state:{
        type:String, 
        required: true
    },
    postalCode : {
        type:String, 
        required: true,
    },  
    customer: {
       type: mongoose.Schema.Types.ObjectId,
       ref : "Customer"
    }, 
})
module.exports = mongoose.model('Address', addressSchema)