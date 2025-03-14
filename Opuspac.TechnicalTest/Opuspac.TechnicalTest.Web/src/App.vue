<template>
  <div>
    <Login v-if="!isLoggedIn" @login-success="handleLoginSuccess" />
    <Produto v-else />
  </div>
</template>

<script>
  import Login from './components/HelloWorld.vue'
  import Produto from './components/TheWelcome.vue'

  export default {
    components: {
      Login,
      Produto
    },
    data() {
      return {
        isLoggedIn: false
      };
    },
    methods: {
      handleLoginSuccess() {
        this.isLoggedIn = true;
      }
    }
  };
</script>
