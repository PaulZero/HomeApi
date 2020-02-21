<template>
    <div class="home">
        <p class="loading-message" v-if="isLoadingLights">Loading lights...</p>

        <p class="error-message" v-if="hasLoadingError">
            The lights could not be loaded: {{ lightLoadingError }}
        </p>

        <div
             class="light-list"
             v-if="areLightsLoaded"
             v-for="light in lights">
            <light-control :light="light"></light-control>
        </div>

        <div v-else>
            Uhm...
        </div>
    </div>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import LightControl from '@/components/lights/LightControl.vue'
    import { ILight } from '@/ts/interfaces/ILight';
    import { LoadingState } from '@/ts/enums/LoadingState';
    import { State } from 'vuex-class';

    @Component({
        components: {
            LightControl
        }
    })
    export default class Home extends Vue {
        @State('lightLoadingState')
        private lightLoadingState!: LoadingState;

        @State('lightLoadingError')
        private lightLoadingError!: string | null;

        @State('lights')
        private lights!: ILight[];

        public async created() {
            await this.$store.dispatch('loadLights');
        }

        public get isLoadingLights() {
            return this.lightLoadingState === LoadingState.Loading;
        }

        public get areLightsLoaded() {
            return this.lightLoadingState === LoadingState.Loaded;
        }

        public get hasLoadingError() {
            return this.lightLoadingState === LoadingState.Error;
        }
    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
    .light-list {
        display: flex;
        flex-direction: column;
    }

    .loading-message {
        font-family: sans-serif;
        font-size: 1.2em;
        text-align: center;
    }

    .error-message {
        font-family: sans-serif;
        font-size: 1.2em;
    }
</style>
