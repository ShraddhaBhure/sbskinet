const proxy = [
    {
      context: '/api',
      target: 'https://localhost:5001',
      secure: false
    }
  ];
  
  module.exports = proxy;
  