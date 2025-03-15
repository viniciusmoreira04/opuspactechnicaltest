<template>
  <div class="dashboard-container">
    <h1 class="text-2xl font-bold mb-4">Dashboard</h1>
    
    <!-- Botões de navegação -->
    <div class="button-group">
      <button @click="toggleSection('create')" class="btn">Criar Produto</button>
      <button @click="toggleSection('products'); fetchProducts()"  class="btn">Lista de Produtos</button>
      <button @click="toggleSection('orders'); fetchOrders()" class="btn">Lista de Ordens de Serviço</button>
    </div>

    <!-- Formulário para criar novo produto -->
    <div v-if="activeSection === 'create'" class="form-container">
      <h2 class="text-lg font-semibold">Criar Produto</h2>
      <input v-model="newProduct.name" type="text" placeholder="Nome" class="input-field" />
      <textarea v-model="newProduct.description" placeholder="Descrição" class="input-field"></textarea>
      <input v-model="newProduct.price" type="number" placeholder="Preço" class="input-field" />
      <button @click="createProduct" class="btn">Criar Produto</button>
    </div>
    
    <!-- Lista de produtos -->
    <div v-if="activeSection === 'products'" class="list-container">
      <ul>
        <li v-for="product in products" :key="product.id" class="list-item">
          <strong>{{ product.name }}</strong> - {{ product.description }} (R$ {{ product.price }})
        </li>
      </ul>
    </div>
    
    <!-- Lista de Ordens de Serviço -->
    <div v-if="activeSection === 'orders'" class="list-container">
      <h2 class="text-lg font-semibold">Lista de Ordens de Serviço</h2>
      <ul>
        <li v-for="order in orders" :key="order.id" class="list-item">
          <strong>Ordem #{{ order.id }}</strong> - {{ order.description }}
        </li>
      </ul>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import api from '@/services/api'

const newProduct = ref({ name: '', description: '', price: '' });
const products = ref([]);
const orders = ref([]);
const activeSection = ref('');

const toggleSection = (section) => {
  activeSection.value = activeSection.value === section ? '' : section;
};

  const fetchProducts = async () => {
    try {
      const response = await api.get('api/products');
      products.value = response.data;
    } catch (error) {
      alert('Falha ao carregar produtos. Tente novamente.');
    }
  };

  const fetchOrders = async () => {
    try {
      const response = await api.get('api/orders');
      products.value = response.data;
    } catch (error) {
      alert('Falha ao carregar produtos. Tente novamente.');
    }
  };

const createProduct = async () => {
  if (!newProduct.value.name || !newProduct.value.description || !newProduct.value.price) {
    alert("Por favor, preencha todos os campos do produto.");
    return;
  }

  try {
    await api.post('api/products', newProduct.value, {
      headers: { 'Content-Type': 'application/json' },
    });
    fetchProducts();
    newProduct.value = { name: '', description: '', price: '' };  
  } catch (error) {
    alert("Ocorreu um erro ao criar o produto.");
  }
};

onMounted(() => {
  fetchProducts();
  fetchOrders();
});
</script>

<style scoped>
.dashboard-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  height: 100vh;
  background: linear-gradient(
    rgba(0, 0, 0, 0.4),
    rgba(0, 0, 0, 0.4)
  ), url('../assets/background.jpg') no-repeat center center;
  background-size: cover;
  padding: 20px;
}

.button-group {
  display: flex;
  gap: 10px;
  margin-bottom: 20px;
}

.form-container, .list-container {
  background: rgba(255, 255, 255, 0.9);
  padding: 20px;
  border-radius: 10px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.3);
  width: 100%;
  max-width: 400px;
  text-align: center;
  margin-bottom: 20px;
}

.input-field {
  display: block;
  width: 100%;
  margin: 0.5rem 0;
  padding: 10px;
  border: 1px solid #ddd;
  border-radius: 5px;
  font-size: 16px;
}

.btn {
  width: auto;
  padding: 10px 15px;
  background-color: #1E90FF;
  color: white;
  border: none;
  border-radius: 5px;
  font-size: 18px;
  font-weight: bold;
  cursor: pointer;
}

.btn:hover {
  background-color: #0056b3;
}

.list-item {
  border: 1px solid #ddd;
  padding: 10px;
  border-radius: 5px;
  margin-bottom: 10px;
  background: rgba(255, 255, 255, 0.8);
}
</style>
