<template>
  <div>
    Players
    <v-list>
      <v-list-item v-for="player in allPlayers" v-bind:key="player.id">
        <v-list-item-avatar>
          <v-img
            :src="'https://avatars0.githubusercontent.com/u/6553283?s=400&u=aece239ee1ee7e7e078ee3018380ba979019a79c&v=4'"
          ></v-img>
        </v-list-item-avatar>
        <v-list-item-content>
          <v-list-item-title v-text="player.name"></v-list-item-title>
        </v-list-item-content>
      </v-list-item>
    </v-list>
  </div>
</template>



<script lang="ts">
import { mapGetters } from "vuex";
import { createProvider } from "../vue-apollo";
import Vue from "vue";
export default Vue.extend({
  computed: {
    ...mapGetters(["allPlayers"])
  }
});
</script>