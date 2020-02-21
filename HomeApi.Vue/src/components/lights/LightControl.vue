<template>
    <div class="light-control">
        <span class="light-name">{{ light.name }}</span>

        <button :class="{enabled: light.isOn}" @click="onClick">{{ powerButtonLabel }}</button>
    </div>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import { ApiClient } from '@/ts/api-client/ApiClient';
    import { ILight } from '@/ts/interfaces/ILight';
import { LightService } from '../../ts/light-service/LightService';

    @Component
    export default class LightControl extends Vue {
        @Prop({ required: true, default: null })
        private light!: ILight;

        private client: ApiClient = new ApiClient();

        private async onClick(): Promise<void> {
            const client = new LightService(new ApiClient());

            await client.toggleLight(this.light);
        }

        public get powerButtonLabel(): string {
            return this.light.isOn ? 'On' : 'Off';
        }
    }
</script>

<style scoped>
    .light-control {
        font-family: sans-serif;
        display: flex;       
    }

    .light-name {
        display: inline-block;
        flex-grow: 1;
        font-size: 1.2em;
    }

    button {
        background-color: pink;
        color: black;
        border: none;
        border-radius: 2px;
        padding: 8px 16px;
        margin: 4px;
        cursor: pointer;
        font-weight: bold;
        font-size: 1.2em;
        min-width: 100px;
    }

    button:focus {
        outline: none;
    }

    button.enabled {
        background-color: greenyellow;
    }
</style>
