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
            <v-data-table :headers="headers" :items="data.matches" :items-per-page="10">
              <template v-slot:top>
                <v-toolbar flat color="white">
                  <v-spacer></v-spacer>
                  <v-btn @click="showAddMatchDialog">Add Match</v-btn>
                </v-toolbar>
              </template>
              <template v-slot:item.winner="{ item }">
                <v-avatar color="indigo" size="32px">
                  <v-img v-if="item.winner.playerA.avatar" :src="item.winner.playerA.avatar"></v-img>
                  <span v-else class="white--text">{{shortName(item.winner.playerA)}}</span>
                </v-avatar> &nbsp;
                <v-avatar color="indigo" size="32px">
                  <v-img v-if="item.winner.playerB.avatar" :src="item.winner.playerB.avatar"></v-img>
                  <span v-else class="white--text">{{shortName(item.winner.playerB)}}</span>
                </v-avatar>
              </template>
              <template v-slot:item.looser="{ item }">
                <v-avatar color="indigo" size="32px">
                  <v-img v-if="item.looser.playerA.avatar" :src="item.looser.playerA.avatar"></v-img>
                  <span v-else class="white--text">{{shortName(item.winner.playerA)}}</span>
                </v-avatar>&nbsp; 
                <v-avatar color="indigo" size="32px">
                  <v-img v-if="item.looser.playerB.avatar" :src="item.looser.playerB.avatar"></v-img>
                  <span v-else class="white--text">{{shortName(item.looser.playerB)}}</span>
                </v-avatar>
              </template>
              <template v-slot:item.timestamp="{ item }">{{ item.timestamp | shortDateTime }}</template>
            </v-data-table>
            <!-- <v-list>
              <v-list-item>
                <v-list-item-content>
                  <v-btn @click="showAddMatchDialog">Add Match</v-btn>
                </v-list-item-content>
              </v-list-item>
              <v-list-item v-for="match in data.matches" v-bind:key="match.id">
                <TeamListItem :team="match.winner" />
                 <span> vs </span> 
                <TeamListItem :team="match.looser" />
                <v-list-item-content>
                  <v-list-item-title></v-list-item-title>
                  <v-list-item-subtitle>{{ match.timestamp | shortDateTime}}</v-list-item-subtitle>
                </v-list-item-content>
                <v-list-item-icon>
                  <v-btn icon @click="deleteMatch(match.id)">
                    <v-icon>mdi-delete</v-icon>
                  </v-btn>
                </v-list-item-icon>
              </v-list-item>
            </v-list>-->
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
              <ApolloQuery :query="require('../graphql/Players.gql')">
                <template v-slot="{ result: { loading, error, data } }">
                  <div v-if="data" class="result apollo">
                    <v-row>
                      <v-col cols="12" md="6">
                        <v-select
                          v-model="winner.player1"
                          :items="data.players"
                          item-text="name"
                          item-value="id"
                          label="Winner - Player 1"
                        ></v-select>
                      </v-col>
                      <v-col cols="12" md="6">
                        <v-select
                          v-model="looser.player1"
                          :items="data.players"
                          item-text="name"
                          item-value="id"
                          label="Looser - Player 1"
                        ></v-select>
                      </v-col>
                    </v-row>
                    <v-row>
                      <v-col cols="12" md="12">
                        <p class="text-center">vs</p>
                      </v-col>
                    </v-row>
                    <v-row>
                      <v-col cols="12" md="6">
                        <v-select
                          v-model="winner.player2"
                          :items="data.players"
                          item-text="name"
                          item-value="id"
                          label="Winner - Player 2"
                        ></v-select>
                      </v-col>
                      <v-col cols="12" md="6">
                        <v-select
                          v-model="looser.player2"
                          :items="data.players"
                          item-text="name"
                          item-value="id"
                          label="Looser - Player 2"
                        ></v-select>
                      </v-col>
                    </v-row>
                  </div>
                </template>
              </ApolloQuery>
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
// import TeamListItem from "./TeamListItem"
export default Vue.extend({
  components: {
    // TeamListItem
  },
  data: () => {
    return {
      dialog: false,
      winner: { player1: null, palyer2: null },
      looser: { player1: null, palyer2: null },
      headers: [
        { text: "Time", value: "timestamp" },
        { text: "Winners", value: "winner", sortable: false },
        { text: "Loosers", value: "looser", sortable: false }
      ]
    };
  },
  methods: {
    shortName: function(player) {
      const parts = player.name.split(" ", 2);
      if (parts.length === 1) return parts[0].substring(0, 2).toUpperCase();
      else
        return parts
          .map(p => p.substring(0, 1))
          .join("")
          .toUpperCase();
    },
    showAddMatchDialog() {
      this.dialog = true;
    },
    saveMatch() {
      const winnerA = this.winner.player1;
      const winnerB = this.winner.player2;
      const looserA = this.looser.player1;
      const looserB = this.looser.player2;
      this.dialog = false;
      const query = require("../graphql/Matches.gql");
      const mutation = require("../graphql/CreateMatch.gql");

      this.$apollo
        .mutate({
          mutation: mutation,
          variables: {
            input: {
              winnerA: winnerA,
              winnerB: winnerB,
              looserA: looserA,
              looserB: looserB
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