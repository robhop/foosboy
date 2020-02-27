<template>
  <div>
    Players
    <ApolloQuery :query="require('../graphql/Players.gql')">
      <template v-slot="{ result: { loading, error, data } }">
        <!-- Loading -->
        <div v-if="loading" class="loading apollo">Loading...</div>

        <!-- Error -->
        <div v-else-if="error" class="error apollo">An error occurred</div>

        <!-- Result -->
        <div v-else-if="data" class="result apollo">
          <v-list>
            <v-list-item v-for="player in data.players" v-bind:key="player.id">
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

        <!-- No result -->
        <div v-else class="no-result apollo">No result :(</div>
      </template>
    </ApolloQuery>
  </div>
</template>
<script lang="ts">
import Vue from "vue";
export default Vue.extend({});
</script>