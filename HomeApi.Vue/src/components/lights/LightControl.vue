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
    }

    .light-name {
        min-width: 150px;
        display: inline-block;
    }

    button {
        background-color: indianred;
        color: black;
        border: none;
        border-radius: 2px;
        padding: 8px 16px;
        margin: 4px;
        cursor: pointer;
    }

    button.enabled {
        color: white;
        background-color: green;
    }
</style>