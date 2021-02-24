
const express = require('express');
const app = express();

const mongoose = require('mongoose')
const env = require('dotenv/config')

const PORT = process.env.PORT || 3000;
app.use(express.json())

const customerRoute = require('./routes/customers')
app.use('/', customerRoute)

app.listen(PORT, () => {
  console.log(`Server is listening on port ${PORT}`);
});
mongoose.connect(process.env.DB, {useNewUrlParser : true,  
                                  useUnifiedTopology: true, 
                                  useCreateIndex: true}, (err)=>{
                                      
    if(err) return console.log(err.message)
    console.log('Database Connected')
})
module.exports = app;