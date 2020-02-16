<template>
    <div class="home">
        <h1>{{ msg }}</h1>
        <p>Welcome to your new single-page application LOLOLOL, built with <a href="https://vuejs.org" target="_blank">Vue.js</a> and <a href="http://www.typescriptlang.org/" target="_blank">TypeScript</a>.</p>
    </div>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import { ApiClient } from '@/ts/api-client/ApiClient';

    @Component
    export default class Home extends Vue {
        @Prop() private msg!: string;

        private client: ApiClient;

        public async created() {
            this.client = new ApiClient();

            const list = await this.client.listLights();
        }
    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
