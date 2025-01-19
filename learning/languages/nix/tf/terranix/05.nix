{ config, lib, options, specialArgs }:
let
  var = options.variable;
  domain = "composablefi.tech";
  zone = "composablefi-tech";
in rec {
  variable = {
    PROJECT = {
      type = "string";
      description = "Google Cloud Project ID";
    };
    IMAGE_FILE = {
      type = "string";
      description = "NixOS image file for Google Cloud";
    };
    NODE_NAME = {
      type = "string";
      description = "VM name";
    };
  };
  data = { google_dns_managed_zone = { env_dns_zone = { name = zone; }; }; };

  resource = {

    google_certificate_manager_dns_authorization = {

      default = {
        name = "dns-auth";
        description = "The default dnss";
        inherit domain;
      };
    };

    google_certificate_manager_certificate_map = {
      certificate_map = { name = zone; };
    };

    google_dns_record_set = {
      dns-auth = {
        type =
          "\${resource.google_certificate_manager_dns_authorization.default.dns_resource_record.0.type}";
        name =
          "\${resource.google_certificate_manager_dns_authorization.default.dns_resource_record.0.name}";
        ttl = 300;
        managed_zone = "\${data.google_dns_managed_zone.env_dns_zone.name}";
        rrdatas = [
          "\${resource.google_certificate_manager_dns_authorization.default.dns_resource_record.0.data}"
        ];
      };

      frontend = {
        name = "${domain}.";
        type = "A";
        ttl = 300;
        managed_zone = "\${data.google_dns_managed_zone.env_dns_zone.name}";
        rrdatas = [ "\${google_compute_address.static.address}" ];
      };

    };
    google_certificate_manager_certificate_map_entry = {
      first_entry = {
        name = "first-entry";
        description = "example certificate map entry";
        map =
          "\${google_certificate_manager_certificate_map.certificate_map.name}";

        certificates =
          [ "\${google_certificate_manager_certificate.root_cert.id}" ];
        hostname = domain;
      };
    };

    # google_compute_target_https_proxy = {
    #   default = {
    #     name = "test-proxy";
    #     url_map = "\${google_compute_url_map.default.id}";
    #     ssl_certificates = [ google_compute_ssl_certificate.default.id ];
    #   };
    # };

    google_certificate_manager_certificate = {
      root_cert = {
        name = "\${ var.PROJECT }";
        description = "The wildcard cert";
        depends_on = [ "resource.google_dns_record_set.dns-auth" ];
        managed = {
          domains = [ domain "*.${domain}" ];
          dns_authorizations = [
            "\${resource.google_certificate_manager_dns_authorization.default.id}"
          ];
        };
      };
    };

    local_file.test_import = {
      filename = "test_import.txt";
      content = "Hello";
    };
    google_service_account = {
      default = {
        account_id = "\${ var.PROJECT }-account";
        project = "\${ var.PROJECT }";
      };
    };

    time_sleep = {
      google_service_account-default = {
        depends_on = [ "resource.google_service_account.default" ];
        create_duration = "30s";
      };
    };

    google_compute_address = { static = { name = "ipv4-address"; }; };

    google_storage_bucket = {
      gce-image = {
        name = "\${var.PROJECT}-gce-image";
        location = "US";
        force_destroy = true;
      };
    };

    google_compute_image = {
      gce-image = {
        name = "gce-image";
        raw_disk = {
          source =
            "\${resource.google_storage_bucket_object.gce-image-gz.self_link}";
        };
      };
    };

    google_compute_firewall = {
      rules = {
        project = "\${var.PROJECT}";
        name = "my-firewall-rule";
        network = "default";
        description = "Creates firewall rule targeting tagged instances";

        allow = {
          protocol = "TCP";
          ports = [ "80" "443" "9000-40000" ];
        };
        direction  = "INGRESS";
        source_ranges  = ["0.0.0.0/0"];
        target_tags = [ "web" ];
      };
    };

    google_storage_bucket_object = {
      gce-image-gz = {
        name = "\${var.PROJECT}-image.tar.gz";
        source = "\${var.IMAGE_FILE}";
        bucket = "\${resource.google_storage_bucket.gce-image.name}";
      };
    };

    google_compute_instance = {
      node = {
        name = "\${var.NODE_NAME}";
        machine_type = "n2-standard-4";

        project = "\${var.PROJECT}";
        depends_on = [ "time_sleep.google_service_account-default" ];
        boot_disk = {
          initialize_params = {
            image = "\${resource.google_compute_image.gce-image.self_link}";
          };
        };

        tags = [ "web" ];
        metadata = { enable-oslogin = true; };

        scratch_disk = { interface = "SCSI"; };

        network_interface = {
          network = "default";
          access_config = {
            nat_ip = "\${google_compute_address.static.address}";
          };
        };

        service_account = {
          email = "\${resource.google_service_account.default.email}";
          scopes = [ "cloud-platform" ];
        };
      };
    };

  };

  provider = {
    google = {
      region = "us-central1";
      zone = "us-central1-c";
      project = "\${ var.PROJECT }";
    };
  };
}
