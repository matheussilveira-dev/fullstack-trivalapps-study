<script setup>
const message = 'Minha primeira interface com Vue.js'
</script>

<template>
  <main class="container">
    <h1>{{ message }}</h1>
    <p>O frontend foi criado e está sendo executado pelo Vite.</p>
  </main>
</template>

<style scoped>
.container {
  max-width: 720px;
  margin: 60px auto;
  padding: 24px;
  font-family: Arial, sans-serif;
}

h1 {
  margin-bottom: 12px;
}
</style>