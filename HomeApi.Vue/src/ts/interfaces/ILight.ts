export interface ILight {
    brightnessPercentage: number;
    colourCoordinates: number[];
    colourMode: string;
    colourTemperature: number | null;
    hue: number | null;
    id: string;
    isOn: boolean;
    isReachable: boolean | null;
    name: string;
    saturation: number | null;
    transitionMilliseconds: number | null;
}
