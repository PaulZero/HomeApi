import axios from "axios";

export class ApiClient {
    private readonly _server = "http://pi-server";

    public async listLights(): Promise<Light[]> | null {
        return await this.get("/api/lights/list-lights");
    }

    private async get(relativeUrl: string): Promise<any> {
        try {
            if (!relativeUrl.startsWith('/')) {
                relativeUrl = `/${relativeUrl}`;
            }

            const url = `${this._server}${relativeUrl}`;

            const response = await axios.get(url);

            console.log("A POST request was made!", response);

            return JSON.parse(response.data);
        } catch (error) {
            console.error("An error occurred!", error);

            return null;
        }
    }
}