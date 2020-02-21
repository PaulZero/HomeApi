import axios from "axios";
import { ILight } from '@/ts/interfaces/ILight';

export class ApiClient {
    private readonly _server: string;

    constructor() {
        this._server = process.env.VUE_APP_HOME_API_URL;
    }

    public async listLights(): Promise<ILight[]> {
        const response = await this.get("/api/lights/list-lights");

        if (response && response.hasOwnProperty("data")) {
            return response.data;
        }

        return [];
    }

    public async setLightState(id: string, brightness: number, isOn: boolean): Promise<void> {
        const params = {
            LightIds: [id],
            Brightness: 255,
            PowerState: isOn,
            TransitionMilliseconds: 100
        };

        await this.post("/api/lights/set-light-state", params);
    }

    private async post(relativeUrl: string, data: any = null): Promise<any> {
        const url = this.prepareUrl(relativeUrl);

        try {
            const response = await axios.post(url, data);

            return response.data;
        } catch (error) {
            console.log(`Could not make POST request to ${url} - ${error}`);
        }
    }

    private async get(relativeUrl: string): Promise<any> {
        const url = this.prepareUrl(relativeUrl);

        try {
            const response = await axios.get(url);

            console.log("A POST request was made!", response);

            return response.data;
        } catch (error) {
            console.log(`Could not make GET request to ${url} - ${error}`);
        }
    }

    private prepareUrl(relativeUrl: string): string {
        if (!relativeUrl.startsWith("/")) {
            relativeUrl = `/${relativeUrl}`;
        }

        return `${this._server}${relativeUrl}`;
    }
}