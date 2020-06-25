import * as React from "react";
import { AreaModel } from "Models/AreaModel";
import Helper from "../functions/Helper";
import "../style/infodata.scss";

interface IAppProps {
  area: AreaModel;
  countries: boolean;
}

interface IAppState {}

export default class AreaInfo extends React.Component<IAppProps, IAppState> {
  public readonly state: IAppState;

  constructor(props: IAppProps) {
    super(props);

    this.state = {};
  }

  public render() {
    return (
      <div
        className={"areainfo-data " + (this.props.countries ? "countries" : "")}
      >
        <h2 className="label">
          {this.props.area.areaName ? this.props.area.areaName : "Selecione"}
        </h2>
        <div className="areas-data ">
          <h2 className="type-data">
            <div className="data">
              <div className="label">Total</div>
              <div className="number color-confirmed">
                {Helper.toNumberFormat(this.props.area.confirmed)}
              </div>
            </div>
            <div className="color bg-color-confirmed"></div>
          </h2>
          <h2 className="type-data">
            <div className="data">
              <div className="label">Ativos</div>
              <div className="number">
                {Helper.toNumberFormat(this.props.area.active)}
              </div>
            </div>
            <div className="color bg-color-active"></div>
          </h2>
          <h2 className="type-data">
            <div className="data">
              <div className="label">Recuperados</div>
              <div className="number">
                {Helper.toNumberFormat(this.props.area.recovered)}
              </div>
            </div>
            <div className="color bg-color-recovered"></div>
          </h2>
          <h2 className="type-data">
            <div className="data">
              <div className="label">Fatais</div>
              <div className="number">
                {Helper.toNumberFormat(this.props.area.deaths)}
              </div>
            </div>
            <div className="color bg-color-deaths"></div>
          </h2>
        </div>
      </div>
    );
  }
}
