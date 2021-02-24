const request = require('supertest');
const app = require('../app');

describe('Home', () => {
    it('Test home response text', async () => {
        const response = await request(app).get('/');
        expect(response.text).toBe('{"body":{"message":"Orders API Home"}}');
    });
    it('Test home status code', async () => {
        const response = await request(app).get('/');
        expect(response.statusCode).toBe(200);
    });  
  });
