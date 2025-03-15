<template>
  <div>
    <h2>Tela de Ordens de Servico</h2>
    <p>Esta é a tela de Ordens de Servico para o teste técnico da Opuspac</p>
  </div>
</template>

<script>
  export default {
    name: "OrdersPage",
    data() {
      return {
        email: '',
        password: ''
      };
    },
    methods: {
      login() {
        this.$emit('login-success');
      }
    }
  };
</script>
