<template>
  <div class="container">
    <h1 class="text-2xl font-bold mb-4">  Criar Novo Produto</h1>
    <form @submit.prevent="submitProduct" class="space-y-4">
      <div>
        <label class="block font-semibold">Nome do Produto</label>
        <input v-model="product.name" type="text" class="border p-2 w-full" required />
      </div>
      <div>
        <label class="block font-semibold">Preço</label>
        <input v-model="product.price" type="number" class="border p-2 w-full" required />
      </div>
      <div>
        <label class="block font-semibold">Descrição</label>
        <textarea v-model="product.description" class="border p-2 w-full"></textarea>
      </div>
      <button type="submit" class="bg-blue-500 text-white px-4 py-2 rounded">Salvar Produto</button>
    </form>
  </div>
</template>


<script>
  import api from '@/services/api'

  export default {
    data() {
      return {
        email: '',
        name: '',
        price: '',
        description: ''
      };
    },
    methods: {
      async submitProduct() {
        try {
          const response = await api.post('api/products', {
            email: this.email,
            name: this.name,
            price: this.price,
            description: this.description
          });
          alert("Produto criado com sucesso!");
          product.value = { name: '', price: '', description: '' }; // Reset do formulário

        } catch (error) {
          this.error = "Houve um erro ao criar o produto."
        }
      }
    }
  };
</script>

<style scoped>
  .container {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    height: 100vh;
    background: linear-gradient( rgba(0,0,0, 0.4), rgba(0,0,0, 0.4) ), url('../assets/background.jpg') no-repeat center center;
    background-size: cover;
  }

  .logo {
    max-width: 280px;
    margin-bottom: 20px;
  }

  form {
    background: rgba(255, 255, 255, 0.9);
    padding: 30px;
    border-radius: 10px;
    box-shadow: 0 0 10px rgba(0,0,0 0.43);
    width: 100%;
    max-width: 400px;
    text-align: center;
  }

  .form-group, h2 {
    margin-bottom: 15px;
  }

  input[type="email"],
  input[type="password"] {
    width: 100%;
    padding: 10px;
    border: 1px solid #ddd;
    border-radius: 5px;
    font-size: 16px;
  }

  button {
    width: 100%;
    padding: 10px;
    background-color: #1E90FF;
    color: #fff;
    border: none;
    border-radius: 5px;
    font-size: 18px;
    font-weight: bold;
    cursor: pointer;
  }

  .error {
    color: red;
    margin-top: 10px;
  }
</style>
