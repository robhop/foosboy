<template>
  <div>
    <ApolloQuery :query="require('../graphql/Matches.gql')">
      <template v-slot="{ result: { loading, error, data } }">
        <!-- Loading -->
        <div v-if="loading" class="loading apollo">Loading...</div>

        <!-- Error -->
        <div v-else-if="error" class="error apollo">An error occurred</div>

        <!-- Result -->
        <div v-else-if="data" class="result apollo">
          <v-card>
            <v-list>
              <v-list-item>
                <v-list-item-content>
                  <v-btn @click="showAddMatchDialog">Add Match</v-btn>
                </v-list-item-content>
              </v-list-item>
              <v-list-item v-for="match in data.matches" v-bind:key="match.id">
                <v-list-item-icon color="indigo">
                  <v-avatar color="indigo">
                    <v-img v-if="match.avatar" :src="match.avatar"></v-img>
                    <span v-else class="white--text headline">{{match.name[0] + match.name[1]}}</span>
                  </v-avatar>
                </v-list-item-icon>
                <v-list-item-content>
                  <v-list-item-title v-text="match.name"></v-list-item-title>
                  <v-list-item-subtitle v-text="match.name"></v-list-item-subtitle>
                </v-list-item-content>
                <v-list-item-icon>
                  <v-btn icon @click="deleteMatch(match.id)">
                    <v-icon>mdi-delete</v-icon>
                  </v-btn>
                </v-list-item-icon>
              </v-list-item>
            </v-list>
          </v-card>
        </div>

        <!-- No result -->
        <div v-else class="no-result apollo">No result :(</div>
      </template>
    </ApolloQuery>
    <v-row justify="center">
      <v-dialog v-model="dialog" persistent max-width="600px">
        <v-card>
          <v-card-title>
            <span class="headline">Match</span>
          </v-card-title>
          <v-card-text>
            <v-container>
              <v-row>
                <v-col cols="12" sm="6" md="4">
                  <v-text-field v-model="name" label="Name" required></v-text-field>
                </v-col>
                <v-col cols="12" sm="6" md="4">
                  <v-text-field v-model="avatar" label="Avatar" hint="url to matches avatar"></v-text-field>
                </v-col>
              </v-row>
            </v-container>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="blue darken-1" text @click.stop="dialog = false">Close</v-btn>
            <v-btn color="blue darken-1" text @click.stop="saveMatch">Save</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-row>
  </div>
</template>
<script lang="javascript">
import Vue from "vue";
// import gql from "graphql-tag";
export default Vue.extend({
  data: () => {
    return {
      dialog: false,
      name: "",
      avatar: ""
    };
  },
  methods: {
    showAddMatchDialog() {
      this.dialog = true;
    },
    saveMatch() {
      this.dialog = false;
      const name = this.name;
      this.name = "";
      const avatar = this.avatar;
      this.avatar = "";
      const query = require("../graphql/Matches.gql");
      const mutation = require("../graphql/CreateMatch.gql");

      this.$apollo
        .mutate({
          mutation: mutation,
          variables: {
            input: {
              name: name,
              avatar: avatar
            }
          },
          update: (store, { data: { createMatch } }) => {
            const data = store.readQuery({
              query: query
            });
            data.matches.push(createMatch);
            store.writeQuery({
              query: query,
              data
            });
          }
        })
        .then(data => {
          console.log(data);
        })
        .catch(error => {
          console.error(error);
        });
    },
    deleteMatch(id) {
      const query = require("../graphql/Matches.gql");
      const mutation = require("../graphql/DeleteMatch.gql");

      this.$apollo
        .mutate({
          mutation: mutation,
          variables: {
            input: {
              id: id
            }
          },
          update: store => {
            const data = store.readQuery({
              query: query
            });
            const index = data.matches.findIndex(m => m.id === id);
            if (index !== -1) {
              data.matches.splice(index, 1);
              store.writeQuery({
                query: query,
                data
              });
            }
          }
        })
        .then(data => {
          console.log(data);
        })
        .catch(error => {
          console.error(error);
        });
    }
  }
});
</script>