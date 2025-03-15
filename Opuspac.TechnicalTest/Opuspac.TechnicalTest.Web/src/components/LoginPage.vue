<template>
  <div class="login-container">
    <img src="@/assets/logo.jpg" alt="Logo da Opuspac" class="logo" />

    <form v-if="!showCreateUserForm" @submit.prevent="login">
      <h2>Acessar sua conta</h2>
      <div class="form-group">
        <input type="email" v-model="email" placeholder="Digite seu email" required />
      </div>
      <div class="form-group">
        <input type="password" v-model="password" placeholder="Digite sua senha" required />
      </div>
      <button type="submit">Entrar</button>
      <p v-if="error" class="error">{{ error }}</p>
      <button type="button" class="create-user-btn" @click="showCreateUserForm = true">
        Criar Usuário
      </button>
    </form>

    <form v-else @submit.prevent="createUser">
      <h2>Criar Nova Conta</h2>
      <div class="form-group">
        <input type="text" v-model="newUser.name" placeholder="Digite seu nome" required />
      </div>
      <div class="form-group">
        <input type="email" v-model="newUser.email" placeholder="Digite seu email" required />
      </div>
      <div class="form-group">
        <input type="password" v-model="newUser.password" placeholder="Digite sua senha" required />
      </div>
      <button type="submit">Criar Conta</button>
      <button type="button" class="cancel-btn" @click="showCreateUserForm = false">
        Cancelar
      </button>
      <p v-if="createUserError" class="error">{{ createUserError }}</p>
    </form>
  </div>
</template>

<script>
  import api from '@/services/api';

  export default {
    name: "LoginPage",
    data() {
      return {
        email: "",
        password: "",
        error: "",
        showCreateUserForm: false, 
        newUser: {
          name: "",
          email: "",
          password: "",
        },
        createUserError: "",
      };
    },
    methods: {
      async login() {
        try {
          const response = await api.post('api/auth/login', {
            email: this.email,
            password: this.password,
          });

          console.log(response.data);
          localStorage.setItem('token', response.data.token);

          this.$router.push('/dashboard');
        } catch (error) {
          this.error = "Credenciais inválidas. Tente novamente!";
        }
      },
      async createUser() {
        try {
          const response = await api.post('api/auth/register', {
            name: this.newUser.name,
            email: this.newUser.email,
            password: this.newUser.password,
          });

          console.log(response.data);
          this.showCreateUserForm = false;
          this.createUserError = ""; 
          alert("Usuário criado com sucesso!");
        } catch (error) {
          this.createUserError = "Erro ao criar usuário. Tente novamente!";
        }
      },
    },
  };
</script>

<style scoped>
  .login-container {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    height: 100vh;
    background: linear-gradient(rgba(0, 0, 0, 0.4), rgba(0, 0, 0, 0.4)), url('../assets/background.jpg') no-repeat center center;
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
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.43);
    width: 100%;
    max-width: 400px;
    text-align: center;
  }

  .form-group,
  h2 {
    margin-bottom: 15px;
  }

  input[type="email"],
  input[type="password"],
  input[type="text"] {
    width: 100%;
    padding: 10px;
    border: 1px solid #ddd;
    border-radius: 5px;
    font-size: 16px;
  }

  button {
    width: 100%;
    padding: 10px;
    background-color: #1e90ff;
    color: #fff;
    border: none;
    border-radius: 5px;
    font-size: 18px;
    font-weight: bold;
    cursor: pointer;
    margin-top: 10px;
  }

  .create-user-btn {
    background-color: #32cd32;
  }

  .cancel-btn {
    background-color: #ff4500;
  }

  .error {
    color: red;
    margin-top: 10px;
  }
</style>
