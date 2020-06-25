import * as React from "react";
import CovidApiService from "../service/CovidApiService";
import Select, { OptionTypeBase } from "react-select";
import "../style/index.scss";
import AreaInfo from "../Componets/AreaInfo";
import { AreaModel } from "Models/AreaModel";
import { CovidModel } from "Models/CovidModel";
import Map from "../Componets/Map";
import Helper from "../functions/Helper";
const coutries = require("../data/country.json");

interface IAppProps {}

interface IAppState {
  global: CovidModel | null;
  selectedArea: AreaModel;
  globalArea: AreaModel;
  selectOptions: OptionTypeBase[];
}

export default class Layout extends React.Component<IAppProps, IAppState> {
  public readonly state: IAppState;

  constructor(props: IAppProps) {
    super(props);
    this.state = {
      selectOptions: [],
      global: null,
      globalArea: {},
      selectedArea: {},
    };
  }

  mapRef: Map | undefined;
  _isMounted: any = false;

  async componentDidMount() {
    this._isMounted = true;
    if (this.state.global == null) {
      this._loadData();
    }
  }

  componentWillUnmount() {
    this._isMounted = false;
  }

  _loadData = async () => {
    let fulldata = await CovidApiService.getLastUpdate().then((res) => {
      return res;
    });
    if (fulldata && this._isMounted) {
      this.setState({
        global: fulldata,
        globalArea: {
          confirmed: fulldata.confirmed,
          active: fulldata.active,
          recovered: fulldata.recovered,
          deaths: fulldata.recovered,
          areaName: "Global",
        },
      });

      let options = fulldata.areas?.map((item) => {
        return {
          label: item.areaName,
          value: item.idAreaHtml,
        } as OptionTypeBase;
      });

      if (options) {
        this.setState({ selectOptions: options });
      }
    }
  };

  _getselectValue = (e: any) => {
    this._filterArea(e.value);
  };

  _filterArea = (areaname: string) => {
    let area = this.state.global?.areas?.find((elem) => {
      return (
        elem.idAreaHtml?.toLocaleLowerCase() === areaname.toLocaleLowerCase()
      );
    });

    console.log(coutries);
    console.log(area?.idAreaHtml);

    if (area) {
      this.setState({ selectedArea: area });
      let _c = coutries.find((x: any) => {
        console.log(x.name, area?.idAreaHtml);

        return (
          x.name.replace(/\s/g, "").toLocaleLowerCase() ===
          area?.idAreaHtml?.toLocaleLowerCase()
        );
      });
      if (_c) {
        this.mapRef?.flyTo(_c.latitude, _c.longitude);
      }
    }
  };

  public render() {
    return (
      <div className="wrapper">
        <header>
          <div className="container ">
            <div className="logo">
              <h1>COVID-19</h1>
              <span>RANKING</span>
              <h2 className="update-date">
                Atualizado: {Helper.dateToString(this.state.global?.create)}
              </h2>
            </div>

            <AreaInfo
              area={this.state.globalArea ? this.state.globalArea : {}}
              countries={false}
            />
          </div>
        </header>
        <section className={"map-wrapper"}>
          <Select
            className="basic-single"
            classNamePrefix="select"
            onChange={(e) => this._getselectValue(e)}
            options={this.state.selectOptions}
          />
          <Map ref={(el) => (this.mapRef = el!)} />
        </section>
        <section className="info-areas">
          <div className="container ">
            <AreaInfo area={this.state.selectedArea} countries={true} />
          </div>
        </section>
        <section className="ranking">
          <div className="container ">
            <h2>Ranking</h2>
            <div className="ranking-info">
              <div className="rk-list">
                <ul>
                  <li>dsfsd</li>
                </ul>
              </div>
              <div className="rk-list">
                <ul>
                  <li>dsfsd</li>
                </ul>
              </div>
              <div className="rk-list">
                <ul>
                  <li>dsfsd</li>
                </ul>
              </div>
              <div className="rk-list">
                <ul>
                  <li>dsfsd</li>
                </ul>
              </div>
            </div>
          </div>
        </section>
      </div>
    );
  }
}
