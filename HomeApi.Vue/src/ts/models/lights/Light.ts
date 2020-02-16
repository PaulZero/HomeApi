class Light {
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

    private readonly _brightnessPercentage: number;
    private readonly _colourCoordinates: number[];
    private readonly _colourMode: string;
    private readonly _colourTemperature: number | null;
    private readonly _hue: number | null;
    private readonly _id: string;
    private readonly _isOn: boolean;
    private readonly _isReachable: boolean | null;
    private readonly _name: string;
    private readonly _saturation: number | null;
    private readonly _transitionMilliseconds: number | null;

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
}
