<template>
    <div class="home">
        <h1>{{ msg }}</h1>
        <p>Welcome to your new single-page application LOLOLOL, built with <a href="https://vuejs.org" target="_blank">Vue.js</a> and <a href="http://www.typescriptlang.org/" target="_blank">TypeScript</a>.</p>

        <div v-for="light in lights">
            <light-control :light="light"></light-control>
        </div>
    </div>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import { ApiClient } from '@/ts/api-client/ApiClient';
    import LightControl from '@/components/lights/LightControl.vue'
    import { Light } from '@/ts/models/lights/Light';

    @Component({
        components: {
            LightControl
        }
    })
    export default class Home extends Vue {
        @Prop() private msg!: string;

        private client: ApiClient = new ApiClient();

        private lights: Light[] | null = null;

        public async created() {
            this.lights = await this.client.listLights();
        }
    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
