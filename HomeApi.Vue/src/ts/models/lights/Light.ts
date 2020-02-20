import { ApiClient } from "@/ts/api-client/ApiClient";

export class Light {
    get brightnessPercentage(): number { return this._brightnessPercentage; }

    get colourCoordinates(): number[] { return this._colourCoordinates; }

    get colourMode(): string { return this._colourMode; }

    get colourTemperature(): number | null { return this._colourTemperature; }

    get hue(): number | null { return this._hue; }

    get id(): string { return this._id; }

    get isOn(): boolean { return this._isOn; }

    get isReachable(): boolean | null { return this._isReachable; }

    get name(): string { return this._name; }

    get saturation(): number | null { return this._saturation; }

    get transitionMilliseconds(): number | null { return this._transitionMilliseconds; }

    private _brightnessPercentage: number;
    private _colourCoordinates: number[];
    private _colourMode: string;
    private _colourTemperature: number | null;
    private _hue: number | null;
    private readonly _id: string;
    private _isOn: boolean;
    private _isReachable: boolean | null;
    private _name: string;
    private _saturation: number | null;
    private _transitionMilliseconds: number | null;

    constructor(
        brightnessPercentage: number,
        colourCoordinates: number[],
        colourMode: string,
        colourTemperature: number | null,
        hue: number | null,
        id: string,
        isOn: boolean,
        isReachable: boolean | null,
        name: string,
        saturation: number | null,
        transitionMilliseconds: number | null
    ) {
        this._brightnessPercentage = brightnessPercentage;
        this._colourCoordinates = colourCoordinates;
        this._colourMode = colourMode;
        this._colourTemperature = colourTemperature;
        this._hue = hue;
        this._id = id;
        this._isOn = isOn;
        this._isReachable = isReachable;
        this._name = name;
        this._saturation = saturation;
        this._transitionMilliseconds = transitionMilliseconds;
    }

    public async togglePower(client: ApiClient): Promise<void> {
        await client.setLightState(this.id, this.brightnessPercentage, !this.isOn);

        this._isOn = !this._isOn;
    }
}
