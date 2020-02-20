<template>
    <div>
        <span class="light-name">{{ light.name }}</span>

        <button @click="onClick">{{ powerButtonLabel }}</button>
    </div>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import { ApiClient } from '@/ts/api-client/ApiClient';
    import { Light } from '@/ts/models/lights/Light';

    @Component
    export default class LightControl extends Vue {
        @Prop({ required: true })
        private light!: Light;

        private client: ApiClient = new ApiClient();

        private async onClick(): Promise<void> {
            await this.light.togglePower(this.client);
        }

        public get powerButtonLabel(): string {
            return this.light.isOn ? 'On' : 'Off';
        }
    }
</script>

<style scoped>
</style>