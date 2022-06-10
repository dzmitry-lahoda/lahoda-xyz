


# CIDR

- specifies fixed bits


# Behind router/NAT

Has next private IP address:

- 10.*/8
- 172.16.*/12 - 172.31.*
- 192.168.*/16



# IPv6 rules

- omit leading zeroes
- :: to omit consecutive zeroes
- ::1 loopback
- ::ffff:0:0/96

# Link local - address up to Router

For configuration before getting real ip:

- 169.254.0.0/16
- fe80::/10

# IPv6 scope and lifetime


# DNS

- UDP
- AAAA-type record


# NAT for ipv4

- Replace private IP range address with public
- Calc checksum
- Save packet sent for that private
- Given response, map packet back to private
- (optional) store custom data in packet to be able to route data back and forth


# Subnet mask

- Bitwice end with outgoing packet
- If equals router network addresss
- Then internal packet
