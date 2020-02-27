<template>
  <div class="home">
    <img alt="Vue logo" src="../assets/logo.png" />
    <HelloWorld msg="Welcome to Your Vue.js App" />
    <Players />
  </div>
</template>

<script>
import HelloWorld from "@/components/HelloWorld.vue";
import Players from "@/components/Players.vue";

export default {
  name: "Home",
  components: {
    HelloWorld,
    Players
  }
};
</script>
