<template>
  <v-list-item-content color="indigo">
    <v-avatar color="indigo" size="12px" >
      <v-img v-if="team.playerA.avatar" :src="team.playerA.avatar"></v-img>
      <span v-else class="white--text headline">{{shortname}}</span>
    </v-avatar>
  </v-list-item-content>
</template>
<script lang="ts">
import Vue from "vue";
export default Vue.extend({
  props: {
    team: Object
  },
  computed: {
    shortname: function() {
      const parts = this.team.playerA.name.split(" ",2);
      if(parts.length === 1)
        return parts[0].substring(0,2).toUpperCase();
      else
        return parts.map(p => p.substring(0,1)).join("").toUpperCase();
    }
  }
});
</script>