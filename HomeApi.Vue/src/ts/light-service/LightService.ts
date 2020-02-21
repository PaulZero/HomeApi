import { ApiClient } from "@/ts/api-client/ApiClient";
import { ILight } from "@/ts/interfaces/ILight";

export class LightService {
    private readonly _apiClient: ApiClient;

    constructor(apiClient: ApiClient) {
        this._apiClient = apiClient;
    }

    public async toggleLight(light: ILight): Promise<ILight> {
        await this._apiClient.setLightState(light.id, light.brightnessPercentage, !light.isOn);

        light.isOn = !light.isOn;

        return light;
    }
}
