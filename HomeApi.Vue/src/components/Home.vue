<template>
    <div class="home">
        <div v-for="light in lights">
            <light-control :light="light"></light-control>
        </div>
    </div>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import { ApiClient } from '@/ts/api-client/ApiClient';
    import LightControl from '@/components/lights/LightControl.vue'
    import { ILight } from '@/ts/interfaces/ILight';

    @Component({
        components: {
            LightControl
        }
    })
    export default class Home extends Vue {
        private client: ApiClient = new ApiClient();

        private lights: ILight[] | null = null;

        public async created() {
            this.lights = await this.client.listLights();
        }
    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
