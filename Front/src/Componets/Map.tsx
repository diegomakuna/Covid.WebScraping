import * as React from 'react';
import mapboxgl from 'mapbox-gl';




//var MapboxLanguage = require('@mapbox/mapbox-gl-language');
var mapRef = document.createElement('div') as HTMLDivElement;
var map : mapboxgl.Map


interface IAppProps {
}
interface IAppState {

    lng: any,
    lat: any,
    zoom: any

}



export default class Map extends React.Component<IAppProps, IAppState> {

    constructor(props: IAppProps) {
        super(props);
        this.state = {
            lng: -55,
            lat: -10,
            zoom: 3,


        }
    }

    

    componentDidMount() {
        map = new mapboxgl.Map({
            container: mapRef,
            style: 'mapbox://styles/mapbox/dark-v10',
            center: [this.state.lng, this.state.lat],
            zoom: this.state.zoom,
            accessToken: "pk.eyJ1IjoiZGllZ29tYWt1bmEiLCJhIjoiY2tidHVkanNoMGRqNTMybWlkZnd3NWZmeCJ9.JyBdoCBR9b1VZfLqdv-n6Q",
        });
        
        // map.addControl(new MapboxLanguage({
        //     defaultLanguage: 'pt'}));
        
    }

    flyTo = (lat:number , long:number) => {
        map.flyTo({
            center: [
                long, 
                lat 
            ],
            essential: true // this animation is considered essential with respect to prefers-reduced-motion
            });
    }

    public render() {
        return (
            <div ref={el => (mapRef = el!)} className='mapContainer' />
        );
    }
}
