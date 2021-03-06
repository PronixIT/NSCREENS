﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <main>
        <div class="container">
            <div class="row">
                <div class="col-sm-6">
                    <div class="row">
                        <div class="col-sm-8">
                            <div id="bts-ex-4" class="selectpicker" data-live="true">
                                <button data-id="prov" type="button" class="btn btn-lg btn-block btn-default dropdown-toggle">
                                    <span class="placeholder">Choose an option</span>
                                    <span class="caret"></span>
                                </button>
                                <div class="dropdown-menu">
                                    <div class="live-filtering" data-clear="true" data-autocomplete="true" data-keys="true">
                                        <label class="sr-only" for="input-bts-ex-4">Search in the list</label>
                                        <div class="search-box">
                                            <div class="input-group">
                                                <span class="input-group-addon" id="search-icon3">
                                                    <span class="fa fa-search"></span>
                                                    <a href="#" class="fa fa-times hide filter-clear"><span class="sr-only">Clear filter</span></a>
                                                </span>
                                                <input type="text" placeholder="Search in the list" id="input-bts-ex-4" class="form-control live-search" aria-describedby="search-icon3" tabindex="1" />
                                            </div>
                                        </div>
                                        <div class="list-to-filter">
                                            <ul class="list-unstyled">
                                                <li class="filter-item items" data-filter="item 1" data-value="1">item 1</li>
                                                <li class="filter-item items" data-filter="item 2" data-value="2">item 2</li>
                                                <li class="filter-item items" data-filter="item 3" data-value="3">item 3</li>
                                                <li class="filter-item items" data-filter="item 4" data-value="4">item 4</li>
                                                <li class="filter-item items" data-filter="item 5" data-value="5">item 5</li>
                                            </ul>
                                            <div class="no-search-results">
                                                <div class="alert alert-warning" role="alert"><i class="fa fa-warning margin-right-sm"></i>No entry for <strong>'<span></span>'</strong> was found.</div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <input type="hidden" name="bts-ex-4" value="">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
    <footer></footer>
    <script src="js/jquery-1.12.3.min.js"></script>
    <script src="js/bootstrap-select.min.js"></script>
    <script src="js/tabcomplete.min.js"></script>
    <script src="js/livefilter.min.js"></script>
    <script src="js/bootstrap-select.min.js"></script>
    <script src="js/filterlist.min.js"></script>
</asp:Content>

