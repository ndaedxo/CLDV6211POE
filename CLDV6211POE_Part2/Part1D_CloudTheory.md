# Part 1D: Cloud Computing Basics

## Question 1: On-Premises vs Cloud Deployment

Deploying an application in the cloud differs significantly from on-premises deployment across three key dimensions: security, deployment speed, and resource management.

### Security

**On-Premises:**
In an on-premises deployment, the organisation is entirely responsible for its own security infrastructure. This includes physical security of servers, network firewalls, intrusion detection systems, and identity management. For example, EventEase would need to purchase and maintain hardware firewalls, manage access control lists manually, and ensure compliance with data protection regulations through internal audits. While this provides complete control over security policies, it also requires dedicated security expertise and constant vigilance.

**Cloud:**
Cloud providers such as Microsoft Azure offer built-in security features that are managed at scale. Azure provides identity management through Microsoft Entra ID (formerly Azure Active Directory), automated encryption of data at rest and in transit, and compliance certifications (ISO 27001, SOC 2, GDPR) that would be costly for a single organisation to achieve independently. For EventEase, this means that user authentication, data encryption, and regulatory compliance are handled by Azure's platform-level security, reducing the burden on the development team. However, the shared responsibility model means EventEase must still secure its application code and configuration.

### Deployment Speed

**On-Premises:**
Deploying on-premises requires procuring physical hardware, setting up network infrastructure, installing operating systems, and configuring middleware before any application can run. This process can take weeks or months. For EventEase, this would mean purchasing servers, configuring racks, and manually installing SQL Server and IIS before the venue booking system could go live.

**Cloud:**
Cloud deployment is virtually instantaneous. Azure App Service can provision a web application environment in minutes, and Azure SQL Database can be created through the portal or CLI with a few clicks. Auto-scaling capabilities mean that if EventEase experiences a surge in booking requests during peak seasons (e.g., holiday periods), additional resources are allocated automatically without manual intervention. This elasticity is a fundamental advantage of cloud computing.

### Resource Management

**On-Premises:**
Resource management on-premises requires capacity planning based on peak load estimates. Hardware must be purchased upfront (capital expenditure), and over-provisioning is common to handle unexpected demand, leading to wasted resources during quiet periods. Maintenance windows are required for hardware upgrades, and scaling requires purchasing and installing additional physical servers.

**Cloud:**
Cloud computing operates on a pay-as-you-go model (operational expenditure), where resources are billed only for what is consumed. Azure provides monitoring tools such as Azure Monitor and Application Insights that give real-time visibility into resource utilisation. For EventEase, this means the venue booking system can scale down during off-peak hours to reduce costs and scale up during busy periods. Microsoft manages the underlying hardware, including patching, replacements, and upgrades, freeing the development team to focus on application features rather than infrastructure maintenance.

---

## Question 2: IaaS, PaaS, and SaaS

### Infrastructure as a Service (IaaS)

IaaS provides the fundamental building blocks of computing infrastructure — virtual machines, storage, and networking — as a service. The cloud provider manages the physical hardware, data centres, and virtualisation layer, while the customer is responsible for the operating system, middleware, runtime, and application.

**Example:** Azure Virtual Machines (VMs) allow EventEase to rent a virtual server running Windows Server with SQL Server installed. The team would still need to manage Windows updates, SQL Server configuration, backups, and security patches on the VM itself.

### Platform as a Service (PaaS)

PaaS provides a complete development and deployment environment in the cloud. The cloud provider manages the infrastructure (servers, storage, networking) and the platform (operating system, middleware, runtime), while the customer focuses solely on the application code and data.

**Example:** Azure App Service allows EventEase to deploy their ASP.NET Core MVC web application directly without managing any servers. Azure handles the underlying infrastructure, operating system patches, load balancing, and SSL certificates. The development team simply publishes their code and Azure takes care of the rest.

### Software as a Service (SaaS)

SaaS delivers a complete, ready-to-use application over the internet. The cloud provider manages everything — infrastructure, platform, and application — and the customer simply uses the software through a web browser or client application.

**Example:** Microsoft Office 365 is a SaaS product. EventEase employees can use Word, Excel, and Outlook without installing or maintaining any software. Similarly, a service like Calendly could be used for basic scheduling without any development effort.

---

## Why PaaS Benefits EventEase Over IaaS and SaaS

EventEase would benefit most from **Platform as a Service (PaaS)** for their venue booking system for the following reasons:

1. **No Server Management Overhead:** With PaaS (Azure App Service), EventEase does not need to manage virtual machines, install operating system updates, or configure web servers. The development team can focus entirely on building booking features, validation logic, and user experience rather than spending time on infrastructure administration.

2. **Auto-Scaling Capabilities:** Azure App Service provides built-in auto-scaling. When EventEase experiences increased demand — such as during wedding season or conference booking periods — the platform automatically provisions additional instances to handle the load. With IaaS, this would require manually configuring scale sets and load balancers.

3. **Pay-as-You-Go Model:** PaaS operates on an operational expenditure model where EventEase pays only for the compute resources consumed. This is more cost-effective than IaaS, where virtual machines must be sized for peak capacity and run continuously, incurring costs even during idle periods.

4. **Faster Development and Deployment:** PaaS enables continuous deployment pipelines. EventEase developers can push code changes directly from their development environment to the live application in minutes. With IaaS, each deployment would require manual server configuration, and with SaaS, the application would be entirely pre-built and not customisable to EventEase's specific booking workflow requirements.

5. **Integrated Services:** Azure App Service integrates seamlessly with other Azure PaaS services such as Azure SQL Database (for data persistence) and Azure Storage (for image management). This creates a cohesive cloud-native architecture without the complexity of managing individual infrastructure components.

In summary, SaaS would not provide the customisation EventEase needs for their specific venue booking workflow, and IaaS would introduce unnecessary infrastructure management overhead. PaaS strikes the ideal balance by providing a managed platform that allows EventEase to focus on their core business — managing venue bookings efficiently.
