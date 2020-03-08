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
          <v-card>
            <v-list>
              <v-list-item>
                <v-list-item-content>
                  <v-btn @click="showAddPlayerDialog">Add Player</v-btn>
                </v-list-item-content>
              </v-list-item>
              <v-list-item v-for="player in data.players" v-bind:key="player.id">
                <PlayerListItemIcon :player="player" />
                <PlayerListItemContent :player="player" />
                <v-list-item-icon>
                  <v-btn icon @click="deletePlayer(player.id)">
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
            <v-btn color="blue darken-1" text @click.stop="dialog = false">Close</v-btn>
            <v-btn color="blue darken-1" text @click.stop="savePlayer">Save</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-row>
  </div>
</template>
<script lang="javascript">
import Vue from "vue";
import PlayerListItemIcon from "./PlayerListItemIcon";
import PlayerListItemContent from "./PlayerListItemContent";
export default Vue.extend({
  components: {
    PlayerListItemIcon,
    PlayerListItemContent
  },
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
      const query = require("../graphql/Players.gql");
      const mutation = require("../graphql/CreatePlayer.gql");

      this.$apollo
        .mutate({
          mutation: mutation,
          variables: {
            input: {
              name: name,
              avatar: avatar
            }
          },
          update: (store, { data: { createPlayer } }) => {
            const data = store.readQuery({
              query: query
            });
            data.players.push(createPlayer);
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
    deletePlayer(id) {
      const query = require("../graphql/Players.gql");
      const mutation = require("../graphql/DeletePlayer.gql");

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
            const index = data.players.findIndex(m => m.id === id);
            if (index !== -1) {
              data.players.splice(index, 1);
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