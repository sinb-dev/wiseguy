<template>
  <div class="ui basic modal">
            
    <md-dialog :md-active.sync="show_login_modal">
        <form novalidate class="md-layout" @submit.prevent="validateUser">
            <md-card class="md-layout-item md-size-100 md-small-size-100">
                <md-card-header>
                <div class="md-title">Log ind</div>
                </md-card-header>

                <md-card-content>
                  <div class="md-layout md-gutter">
                    <div class="md-layout-item md-small-size-100">
                    <md-field :class="getValidationClass('username')">
                        <label for="username">Brugernavn</label>
                        <md-input name="username" id="username" autocomplete="given-name" v-model="form.username" :disabled="sending" />
                        <span class="md-error" v-if="!$v.form.username.required">Brugernavn mangler</span>
                        <span class="md-error" v-else-if="!$v.form.username.minlength">Ugyldigt brugernavn</span>
                    </md-field>
                    </div>

                    <div class="md-layout-item md-small-size-100">
                    <md-field :class="getValidationClass('password')">
                        <label for="password">Kodeord</label>
                        <md-input name="password" type="password" id="password" autocomplete="family-name" v-model="form.password" :disabled="sending" />
                        <span class="md-error" v-if="!$v.form.password.required">Mangler kodeord</span>
                        <span class="md-error" v-else-if="!$v.form.password.minlength">Kodeordet mangler</span>
                    </md-field>
                    </div>
                  </div>
                </md-card-content>

                <md-card-actions>
                <md-button type="submit" class="md-primary" :disabled="sending">Log ind</md-button>
                </md-card-actions>
            </md-card>

            <md-snackbar :md-active.sync="loggedIn">The user {{ lastUser }} was saved with success!</md-snackbar>
            </form>
    </md-dialog>
    <!--<md-button class="md-primary md-raised" @click="showDialog = true">Show Dialog</md-button>-->
            
  </div>
</template>

<script>
import { validationMixin } from 'vuelidate'
  import {
    required,
    minLength,
  } from 'vuelidate/lib/validators'
export default {

    name: 'FormValidation',
    mixins: [validationMixin],
    data: () => ({
      form: {
        username: null,
        password: null
      },
      loggedIn: false,
      sending: false,
      lastUser: null,
      show_login_modal : true
    }),
    validations: {
      form: {
        username: {
          required,
          minLength: minLength(3)
        },
        password: {
          required,
          minLength: minLength(3)
        }
      }
    },
    methods: {
      getValidationClass (fieldName) {
        const field = this.$v.form[fieldName]

        if (field) {
          return {
            'md-invalid': field.$invalid && field.$dirty
          }
        }
      },
      clearForm () {
        this.$v.$reset()
        this.form.username = null
        this.form.password = null
      },
      saveUser () {
        this.sending = true

        // Instead of this timeout, here you can call your API
        this.$root.logUserIn(this.form.username,this.form.password);
      },
      validateUser () {
        this.$v.$touch()

        if (!this.$v.$invalid) {
          this.saveUser()
        }
      }
    },
    mounted() {

    }
  }
</script>

<style>

</style>