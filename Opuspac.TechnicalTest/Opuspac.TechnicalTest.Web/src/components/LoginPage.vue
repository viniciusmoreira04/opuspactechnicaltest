<template>
  <div class="login-container">
    <img src="@/assets/logo.jpg" alt="Logo da Opuspac" class="logo" />

    <form @submit.prevent="login">
      <h2>Acessar sua conta</h2>
      <div class="form-group">
        <input type="email" v-model="email" placeholder="Digite seu email" required />
      </div>
      <div class="form-group">
        <input type="password" v-model="password" placeholder="Digite sua senha" required />
      </div>
      <button type="submit">Entrar</button>
      <p v-if="error" class="error">{{error}}</p>
    </form>
  </div>
</template>

<script>
  import api from '@/services/api'

  export default {
    name: "LoginPage",
    data() {
      return {
        email: "",
        password: "",
        error: "",
      };
    },
      methods: {
        async login() {
          try {
            const response = await api.post('api/auth/login', {
              email: this.email,
              password: this.password
            });

            console.log(response.data)
            localStorage.setItem('token', response.data.token)

            this.$router.push('/dashboard');
          } catch (error) {
            this.error = "Credenciais inválidas. Tente novamente!"
          }
        }
      }
};
</script>

<style scoped >
  .login-container {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    height: 100vh;
    background: linear-gradient(
        rgba(0,0,0, 0.4),
        rgba(0,0,0, 0.4)
    ), url('../assets/background.jpg') no-repeat center center;
    background-size: cover;
  }

  .logo{
      max-width: 280px;
      margin-bottom: 20px;
  }

  form{
      background: rgba(255, 255, 255, 0.9);
      padding: 30px;
      border-radius: 10px;
      box-shadow: 0 0 10px rgba(0,0,0 0.43);
      width: 100%;
      max-width: 400px;
      text-align: center;
  }

  .form-group, h2{
      margin-bottom: 15px;
  }

  input[type="email"],
  input[type="password"]{
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

  .error{
      color: red;
      margin-top: 10px;
  }

</style>
