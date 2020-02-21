import Vue from 'vue'
import Vuex from 'vuex'
import { LoadingState } from '@/ts/enums/LoadingState';
import { ApiClient } from '@/ts/api-client/ApiClient';

Vue.use(Vuex);

export default new Vuex.Store({
    state: {
        lightLoadingState: LoadingState.NotLoaded,
        lightLoadingError: null,
        lights: [],
    },
    mutations: {
    },
    actions: {
        async loadLights(context) {
            try {
                context.state.lightLoadingState = LoadingState.Loading;
                context.state.lightLoadingError = null;

                const client = new ApiClient();
                const lights = await client.listLights();

                if (lights.length > 0) {
                    context.state.lights = lights as never[];
                    context.state.lightLoadingState = LoadingState.Loaded;

                    return;
                }

                context.state.lightLoadingState = LoadingState.NotLoaded;
            } catch (error) {
                context.state.lightLoadingState = LoadingState.Error;
                context.state.lightLoadingError = error;
            }
        }
    }
});
