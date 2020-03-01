<template>
  <div>
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
                <!-- <v-img :src="player.avatar"></v-img> -->
              </v-list-item-avatar>
              <v-list-item-content>
                <v-list-item-title v-text="player.name"></v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <v-list-item>
              <v-list-item-content>
                <v-btn v-on:click="showAddPlayerDialog">Add Player</v-btn>
              </v-list-item-content>
            </v-list-item>
          </v-list>
        </div>

        <!-- No result -->
        <div v-else class="no-result apollo">No result :(</div>
      </template>
    </ApolloQuery>
    <v-row justify="center">
      <v-dialog v-model="dialog" persistent max-width="600px">
        <v-card>
          <v-card-title>
            <span class="headline">Player</span>
          </v-card-title>
          <v-card-text>
            <v-container>
              <v-row>
                <v-col cols="12" sm="6" md="4">
                  <v-text-field v-model="name" label="Name" required></v-text-field>
                </v-col>
                <v-col cols="12" sm="6" md="4">
                  <v-text-field v-model="avatar" label="Avatar" hint="url to players avatar"></v-text-field>
                </v-col>
              </v-row>
            </v-container>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="blue darken-1" text @click="dialog = false">Close</v-btn>
            <v-btn color="blue darken-1" text @click="savePlayer">Save</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-row>
  </div>
</template>
<script lang="ts">
import Vue from "vue";
import gql from "graphql-tag";

export default Vue.extend({
  data: () => {
    return {
      dialog: false,
      name: "",
      avatar: ""
    };
  },
  methods: {
    showAddPlayerDialog() {
      this.dialog = true;
    },
    savePlayer() {
      this.dialog = false;
      const name = this.name;
      this.name = "";
      const avatar = this.avatar;
      this.avatar = "";

      this.$apollo
        .mutate({
          // Query
          mutation: gql`
            mutation createPlayer($input: PlayerInput!) {
              createPlayer(input: $input) {
                id
                name
                avatar
              }
            }
          `,
          // Parameters
          variables: {
            input: {
              name: name,
              avatar: avatar
            }
          },
          update: (store, { data: { createPlayer } }) => {
            const data = store.readQuery({
              query: require("../graphql/Players.gql")
            });
            data.players.push(createPlayer);
            store.writeQuery({
              query: require("../graphql/Players.gql"),
              data
            });
          }
          // Optimistic UI
          // Will be treated as a 'fake' result as soon as the request is made
          // so that the UI can react quickly and the user be happy
          // optimisticResponse: {
          //   __typename: "Mutation",
          //   addTag: {
          //     __typename: "Tag",
          //     id: -1,
          //     label: newTag
          //   }
          // }
        })
        .then(data => {
          // Result
          console.log(data);
        })
        .catch(error => {
          // Error
          console.error(error);
          // We restore the initial user input
          // this.newTag = newTag;
        });
    }
  }
});
</script>