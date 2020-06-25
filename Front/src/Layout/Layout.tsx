import * as React from "react";
import CovidApiService from "../service/CovidApiService";
import Select, { OptionTypeBase } from "react-select";
import "../style/index.scss";
import AreaInfo from "../Componets/AreaInfo";
import { AreaModel } from "Models/AreaModel";
import { CovidModel } from "Models/CovidModel";
import Map from "../Componets/Map";
import Helper from "../functions/Helper";
import _ from "lodash";
import Moment from 'react-moment';
const coutries = require("../data/country.json");

interface IAppProps {}

interface IAppState {
  global: CovidModel | null;
  selectedArea: AreaModel;
  globalArea: AreaModel;
  selectOptions: OptionTypeBase[];
  topConfirmed: AreaModel[];
  topActive: AreaModel[];
  topRecovered: AreaModel[];
  topDeaths: AreaModel[];
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
      topConfirmed: [],
      topActive: [],
      topRecovered: [],
      topDeaths: [],
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

      this._top10Filter();
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

    if (area) {
      this.setState({ selectedArea: area });
      let _c = coutries.find((x: any) => {
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

  _top10Filter() {
   if (this.state.global?.areas && this.state.global?.areas.length > 0) {
      this.setState({
        topConfirmed: _.orderBy(
          this.state.global.areas,
          ({ confirmed }) => confirmed || "",
          ["desc"]
        ).slice(0, 10),
        topActive: _.orderBy(
          this.state.global.areas,
          ({ active }) => active || "",
          ["desc"]
        ).slice(0, 10),
        topRecovered: _.orderBy(
          this.state.global.areas,
          ({ recovered }) => recovered || "",
          ["desc"]
        ).slice(0, 10),
        topDeaths: _.orderBy(
          this.state.global.areas,
          ({ deaths }) => deaths || "",
          ["desc"]
        ).slice(0, 10),
      });
      
    }
  }

  public render() {
    return (
      <div className="wrapper">
        <header>
          <div className="container ">
            <div className="logo">
              <h1>COVID-19</h1>
              <span>RANKING</span>
              <h2 className="update-date">
                Atualizado em  <Moment format="DD/MM/YYYY HH:MM">{this.state.global?.create}</Moment>
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
            <div className="title">
              <h2>Ranking</h2>
            </div>
            <div className="ranking-info">
              <div className="rk-list confirmed">
                <h3>Confirmados</h3>
                <ol>
                  {this.state.topConfirmed.map((item, index) => {
                    return (
                      <li key={index}>
                        <span>{item.areaName}</span>
                        <span>{Helper.toNumberFormat(item.confirmed)}</span>
                      </li>
                    );
                  })}
                </ol>
              </div>
              <div className="rk-list active">
              <h3>Ativos</h3>
                <ol>
                  {this.state.topActive.map((item, index) => {
                    return (
                      <li key={index}>
                        <span>{item.areaName}</span>
                        <span>{Helper.toNumberFormat(item.active)}</span>
                      </li>
                    );
                  })}
                </ol>
              </div>
              <div className="rk-list recovered">
              <h3>Recuperados</h3>
                <ol>
                  {this.state.topRecovered.map((item, index) => {
                    return (
                      <li key={index}>
                        <span>{item.areaName}</span>
                        <span>{Helper.toNumberFormat(item.recovered)}</span>
                      </li>
                    );
                  })}
                </ol>
              </div>
              <div className="rk-list deaths">
              <h3>Fatais</h3>
                <ol>
                  {this.state.topDeaths.map((item, index) => {
                    return (
                      <li key={index}>
                        <span>{item.areaName}</span>
                        <span>{Helper.toNumberFormat(item.deaths)}</span>
                      </li>
                    );
                  })}
                </ol>
              </div>
            </div>
          </div>
        </section>
        <footer>
          <div className="container">
           <a href="http://araujodiego.com.br"> by Diego Araujo</a> 
          </div>
        </footer>
      </div>
    );
  }
}
