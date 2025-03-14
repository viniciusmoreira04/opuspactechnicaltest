<template>
  <div class="flex min-h-screen items-center justify-center bg-gray-100">
    <div class="w-full max-w-md p-8 bg-white shadow-lg rounded-xl">
      <h2 class="text-2xl font-bold text-center mb-6">Login</h2>
      <form @submit.prevent="handleLogin">
        <div class="mb-4">
          <label class="block text-gray-700" for="email">Email</label>
          <input v-model="email"
                 type="email"
                 id="email"
                 class="w-full px-4 py-2 mt-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-400"
                 required />
        </div>
        <div class="mb-4">
          <label class="block text-gray-700" for="password">Senha</label>
          <input v-model="password"
                 type="password"
                 id="password"
                 class="w-full px-4 py-2 mt-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-400"
                 required />
        </div>
        <button type="submit"
                class="w-full bg-blue-500 text-white py-2 rounded-lg hover:bg-blue-600 transition">
          Entrar
        </button>
      </form>
    </div>
  </div>
</template>

<script>
  export default {
    data() {
      return {
        email: '',
        password: ''
      };
    },
    methods: {
      async handleLogin() {
        try {
          const response = await fetch('https://localhost:7138/login', {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json'
            },
            body: JSON.stringify({
              email: this.email,
              password: this.password
            })
          });

          if (!response.ok) {
            throw new Error('Credenciais inválidas');
          }

          const data = await response.json();
          console.log('Login bem-sucedido:', data);

          this.$emit('login-success');
        } catch (error) {
          console.error('Erro ao fazer login:', error);
          alert('Falha ao fazer login. Verifique suas credenciais.');
        }
      }
    }
  };
</script>
