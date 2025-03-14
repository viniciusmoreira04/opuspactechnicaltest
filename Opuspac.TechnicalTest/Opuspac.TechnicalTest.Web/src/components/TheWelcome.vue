<template>
  <div class="flex min-h-screen items-center justify-center bg-gray-100">
    <div class="w-full max-w-md p-8 bg-white shadow-lg rounded-xl">
      <h2 class="text-2xl font-bold text-center mb-6">Criar Produto</h2>
      <form @submit.prevent="createProduct">
        <div class="mb-4">
          <label class="block text-gray-700" for="productName">Nome do Produto</label>
          <input v-model="product.name"
                 type="text"
                 id="productName"
                 class="w-full px-4 py-2 mt-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-400"
                 required />
        </div>
        <div class="mb-4">
          <label class="block text-gray-700" for="productPrice">Preço</label>
          <input v-model="product.price"
                 type="number"
                 id="productPrice"
                 class="w-full px-4 py-2 mt-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-400"
                 required />
        </div>
        <button type="submit"
                class="w-full bg-green-500 text-white py-2 rounded-lg hover:bg-green-600 transition">
          Criar Produto
        </button>
      </form>
    </div>
  </div>
</template>

<script>
  export default {
    data() {
      return {
        product: {
          name: '',
          price: ''
        }
      };
    },
    methods: {
      async createProduct() {
        try {
          const response = await fetch("/api/produtos", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(this.product)
          });
          if (!response.ok) throw new Error("Erro ao criar produto");
          alert("Produto criado com sucesso!");
        } catch (error) {
          console.error(error);
          alert("Falha ao criar produto");
        }
      }
    }
  };
</script>
