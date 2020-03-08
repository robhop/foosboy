<template>
  <v-list-item-icon color="indigo">
    <v-avatar color="indigo">
      <v-img v-if="player.avatar" :src="player.avatar"></v-img>
      <span v-else class="white--text headline">{{shortname}}</span>
    </v-avatar>
  </v-list-item-icon>
</template>
<script lang="ts">
import Vue from "vue";
export default Vue.extend({
  props: {
    player: Object
  },
  computed: {
    shortname: function() {
      const parts = this.player.name.split(" ",2);
      if(parts.length === 1)
        return parts[0].substring(0,2).toUpperCase();
      else
        return parts.map(p => p.substring(0,1)).join("").toUpperCase();
    }
  }
});
</script>