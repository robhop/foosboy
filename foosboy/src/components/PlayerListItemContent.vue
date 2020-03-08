<template>
  <v-list-item-content>
    <v-list-item-title v-text="player.name"></v-list-item-title>
    <v-list-item-subtitle v-text="player.name"></v-list-item-subtitle>
  </v-list-item-content>
</template>
<script lang="ts">
import Vue from "vue";
export default Vue.extend({
  props: {
    player: Object
  }
});
</script>